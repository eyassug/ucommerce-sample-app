﻿using System;
using System.IO;
using UCommerce.DocsCompiler;
using UCommerce.DocsCompiler.Output;

namespace Compiler.Runner
{
	public class Program
	{
		private static readonly string DocsOutput = @"C:\Projects\ucommerce-sample-app\documentation\SampleApp";

		public static void Main(string[] args)
		{
			var rootPath = Path.GetFullPath("./../../../../../");

			Generate(rootPath, OutputType.Html);

			// Copying resource files to docs output root
			var resourcesPath = Path.Combine(rootPath, "Tools", "Documentation Template");

			var resourceDest = Path.Combine(DocsOutput, @"..\");
			foreach (var resourceDirectory in Directory.GetDirectories(resourcesPath, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(resourceDirectory.Replace(resourcesPath, resourceDest));
			}

			foreach (var resourceFile in Directory.GetFiles(resourcesPath, "*", SearchOption.AllDirectories))
			{
				var destFile = resourceFile.Replace(resourcesPath, resourceDest);

				if (File.Exists(destFile))
				{
					File.Delete(destFile);
				}

				File.Copy(resourceFile, destFile);
			}

			Console.ReadLine();
		}

		private static void Generate(string rootPath, OutputType outputType)
		{
			var docsPath = Path.Combine(rootPath, "src/SampleApp.Documentation");

			var outputPath = Path.Combine(DocsOutput);
			var output = CreateDocumentationOutputSpecification(rootPath, outputPath, outputType);

			try
			{
				Console.WriteLine("Generating documentation using docsPath: {0} and output path {1}", docsPath, outputPath);
				output.Versions = new string[0];

				UCommerce.DocsCompiler.Compiler.CompileFolder(output, docsPath, "Home", "");
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
				Console.WriteLine("Press any key to continue");
				Console.ReadLine();
			}

			Console.WriteLine("Done");
		}

		private static IDocsOutput CreateDocumentationOutputSpecification(string rootPath, string outputPath, OutputType outputType)
		{
			if (outputType == OutputType.Html)
			{
				IDocsOutput output = new HtmlDocsOutput
				{
					ContentType = OutputType.Html,
					OutputPath = outputPath,
					PageTemplate =
						File.ReadAllText(
							Path.Combine(rootPath, @"Tools\documentation-html-template.html")),
					RootUrl = "/sampleapp/",
					ImagesPath = "images/",
				};
				return output;
			}

			return null;
		}
	}
}