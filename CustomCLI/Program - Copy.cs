////fib bundle --o my-file.txt --l c# --n --s extension --rel --a "John Doe"
//using System.CommandLine;
//using System.IO;
//using System.Linq;

//var bundleOption = new Option<FileInfo>("--o", "File path and name")
//{
//    IsRequired = true
//};

//var languageOption = new Option<string[]>(
//    "--l",
//    description: "List of programming languages (e.g., C#, Java, Python, etc.). Use 'all' for all languages.",
//    parseArgument: result => result.Tokens.Select(t => t.Value).ToArray()
//)
//{
//    IsRequired = true
//};

//var noteOption = new Option<bool>("--n", "Include source file paths as comments in the bundle");

//var sortOption = new Option<string>(
//    "--s",
//    description: "Sort files by 'name' (alphabetically by file name) or 'extension' (by file type). Default is 'name'.",
//    getDefaultValue: () => "name"
//);

//var removeEmptyLinesOption = new Option<bool>(
//    "--rel",
//    description: "Remove empty lines from source code before bundling"
//);

//var authorOption = new Option<string>(
//    "--a",
//    description: "Specify the author of the bundle (adds an author comment at the top of the file)"
//);

//var bundleCommand = new Command("bundle", "Bundle code files to a single file");
//bundleCommand.AddOption(bundleOption);
//bundleCommand.AddOption(languageOption);
//bundleCommand.AddOption(noteOption);
//bundleCommand.AddOption(sortOption);
//bundleCommand.AddOption(removeEmptyLinesOption);
//bundleCommand.AddOption(authorOption);

//var languageExtensions = new Dictionary<string, string>
//{
//    { ".cs", "c#" },
//    { ".java", "java" },
//    { ".js", "javascript" },
//    { ".ts", "typescript" },
//    { ".py", "python" },
//    { ".html", "html" },
//    { ".css", "css" },
//    { ".sql", "sql" },
//    { ".sh", "bash" },
//    { ".ps1", "powershell" },
//    { ".json", "json" },
//    { ".xml", "xml" }
//};

//bundleCommand.SetHandler(
//    async (FileInfo output, string[] languages, bool note, string sort, bool removeEmptyLines, string author) =>
//    {
//        if (output == null || string.IsNullOrEmpty(output.FullName))
//        {
//            Console.WriteLine("Error: Invalid output file specified.");
//            return;
//        }

//        if (languages == null || !languages.Any())
//        {
//            Console.WriteLine("Error: No languages specified.");
//            return;
//        }

//        try
//        {
//            if (File.Exists(output.FullName))
//            {
//                Console.WriteLine("Error: Output file already exists. Please choose a different name.");
//                return;
//            }

//            var filesToInclude = Directory.GetFiles(".", "*", SearchOption.AllDirectories)
//                .Where(file =>
//                {
//                    var extension = Path.GetExtension(file).ToLower();
//                    return languages.Contains("all") ||
//                           languages.Any(lang => languageExtensions.TryGetValue(extension, out var matchedLang) && lang.Equals(matchedLang, StringComparison.OrdinalIgnoreCase));
//                })
//                .ToArray();

//            if (!filesToInclude.Any())
//            {
//                Console.WriteLine("No files found for the specified languages.");
//                return;
//            }

//            if (sort == "extension")
//            {
//                filesToInclude = filesToInclude.OrderBy(file => Path.GetExtension(file)).ThenBy(file => Path.GetFileName(file)).ToArray();
//            }
//            else
//            {
//                filesToInclude = filesToInclude.OrderBy(file => Path.GetFileName(file)).ToArray();
//            }

//            using var writer = new StreamWriter(output.FullName);

//            // הוספת שם היוצר בראש הקובץ
//            if (!string.IsNullOrWhiteSpace(author))
//            {
//                await writer.WriteLineAsync($"//       Author: {author}");
//            }

//            foreach (var file in filesToInclude)
//            {
//                if (note)
//                {
//                    var relativePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), file);
//                    await writer.WriteLineAsync($"// Source file: {relativePath}");
//                }

//                Console.WriteLine($"Adding file: {file}");
//                var content = await File.ReadAllTextAsync(file);

//                if (removeEmptyLines)
//                {
//                    content = RemoveEmptyLines(content);
//                }

//                await writer.WriteLineAsync(content);
//            }

//            Console.WriteLine("Bundling process completed successfully!");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error: {ex.Message}");
//        }
//    },
//    bundleOption, languageOption, noteOption, sortOption, removeEmptyLinesOption, authorOption);

//var rootCommand = new RootCommand("Root command for bundler CLI");
//rootCommand.AddCommand(bundleCommand);

//await rootCommand.InvokeAsync(args);

//string RemoveEmptyLines(string content)
//{
//    return string.Join("\n", content.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)));
//}
