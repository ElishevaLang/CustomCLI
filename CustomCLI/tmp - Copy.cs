//using System.CommandLine;
//using System.IO;
//using System.Linq;

//var bundleOption = new Option<FileInfo>("--output", "File path and name");
//var languageOption = new Option<string[]>("--language", "A list of programming languages to include, or 'all' to include all.")
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



//bundelCommand.SetHandler((output, languages) =>
//{
//    try
//    {
//        if (File.Exists(output.FullName))
//        {
//            Console.WriteLine("Error: The file already exists. Please choose a different name.");
//            return;
//        }
//        // Create the file
//        using (var stream = File.Create(output.FullName))
//        {
//            Console.WriteLine("File was created");
//        }

//        //כתיבה לקובץ
//        //using (var writer = new StreamWriter(output.FullName, true)) 
//        //{
//        //    writer.WriteLine("successsssss");
//        //}

//        //Console.WriteLine("Successfully wrote to the file.");
//        var filesToInclude = Directory.GetFiles(".", "*", SearchOption.AllDirectories)
//            .Where(file =>
//            {
//                var extension = Path.GetExtension(file).ToLower();
//                return languages.Contains("all") ||
//                       languages.Any(lang => languageExtensions.TryGetValue(extension, out var langMatch) && langMatch == lang);
//            });

//        if (!filesToInclude.Any())
//        {
//            Console.WriteLine("No files matched the specified languages.");
//            return;
//        }
//        //=================================================
//        using (var writer = new StreamWriter(output.FullName, true)) // Append mode
//        {
//            foreach (var file in filesToInclude)
//            {
//                Console.WriteLine($"Adding file: {file}");
//                var content = File.ReadAllText(file);
//                writer.WriteLine(content);
//            }
//        }
//        Console.WriteLine("Bundling completed successfully!");

//    }

//    catch (DirectoryNotFoundException ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//    }

//}, bundleOption, languageOption);

//var rootCommand = new RootCommand("Root command for bundler CLI ");
//rootCommand.AddCommand(bundelCommand);

//rootCommand.InvokeAsync(args);
