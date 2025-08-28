using UnityEditor;
using UnityEditor.Callbacks;
using System.Xml;
using System.IO;

public class CsprojPostProcessor
{
	[PostProcessBuild]
	public static void OnPostprocessBuild()
	{
		// Opcjonalnie do buildów - my skupiamy się na edytorze
	}

	[InitializeOnLoadMethod]
	private static void SetupCsprojWatcher()
	{
		// Nasłuchuje zmiany w .csproj po regeneracji
		EditorApplication.update += HideAsmdefInCsproj;
	}

	private static bool processed = false;

	private static void HideAsmdefInCsproj()
	{
		if (processed) return;

		string[] csprojFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj", SearchOption.TopDirectoryOnly);

		foreach (var csprojPath in csprojFiles)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(csprojPath);

			XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
			ns.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");

			bool modified = false;

			foreach (XmlNode node in doc.SelectNodes("//msb:Compile", ns))
			{
				if (node.Attributes["Include"] != null &&
					(node.Attributes["Include"].Value.EndsWith(".asmdef") || node.Attributes["Include"].Value.EndsWith(".asmref")))
				{
					if (node["Visible"] == null)
					{
						XmlElement visibleElem = doc.CreateElement("Visible", doc.DocumentElement.NamespaceURI);
						visibleElem.InnerText = "False";
						node.AppendChild(visibleElem);
						modified = true;
					}
				}
			}

			if (modified)
			{
				doc.Save(csprojPath);
			}
		}

		processed = true;
	}
}
