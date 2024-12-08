//using System.CommandLine;
//using System.IO;
//using System.Linq;

//var bundleOption = new Option<FileInfo>("--output", "File path and name");
//var languageOption = new Option<string>("--language", "List of programming languages (e.g., C#, Java, Python, etc.)")
//{
//    IsRequired = true
//};

//var bundelCommand = new Command("bundle", "Bundle code files to a single file");
//bundelCommand.AddOption(bundleOption);
//bundelCommand.AddOption(languageOption);


//var languageExtensions = new Dictionary<string, string>
//{
//    { ".cs", "c#" },
//    { ".java", "java" },
//    // ... rest of the language extensions
//};

//bundelCommand.SetHandler(async context => // Destructure arguments from context
//{
//    var (outputFile, targetLanguages) = context.ParseResult; //Tuple with output and languages

//    try
//    {
//        var outputFilePath = outputFile.FullName;

//        // Check if output file already exists
//        if (File.Exists(outputFilePath))
//        {
//            Console.WriteLine("Error: Output file already exists. Please choose a different name.");
//            return;
//        }

//        var filesToInclude = GetFilesToBundle(targetLanguages, outputFile); // Pass output file

//        // Check if any files were found
//        if (!filesToInclude.Any())
//        {
//            Console.WriteLine("No files found matching the specified languages.");
//            return;
//        }

//        // Write bundled content to output file
//        using (var writer = new StreamWriter(outputFilePath))
//        {
//            foreach (var file in filesToInclude)
//            {
//                Console.WriteLine($"Adding file: {file}");
//                var content = await File.ReadAllTextAsync(file);
//                await writer.WriteLineAsync(content);
//            }
//        }

//        Console.WriteLine("Bundling process completed successfully!");
//    }
//    catch (Exception ex) when (ex is DirectoryNotFoundException || ex is IOException)
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//    }
//});

//IEnumerable<string> GetFilesToBundle(string targetLanguages, FileInfo outputFile)
//{
//    var allLanguages = targetLanguages.Equals("all", StringComparison.OrdinalIgnoreCase);
//    return Directory.GetFiles(".", "*", SearchOption.AllDirectories)
//        .Where(file =>
//        {
//            var extension = Path.GetExtension(file).ToLower();
//            return (allLanguages || languageExtensions.ContainsKey(extension) && languageExtensions[extension] == targetLanguages) &&
//                   !file.Equals(outputFile.FullName, StringComparison.OrdinalIgnoreCase);
//        });
//}

//var rootCommand = new RootCommand("Root command for bundler CLI ");
//rootCommand.AddCommand(bundelCommand);

//rootCommand.InvokeAsync(args);