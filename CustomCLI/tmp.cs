
//using System.CommandLine;
//using System.CommandLine.Parsing;
//using System.IO;
//using System.Linq;

//var bundleOption = new Option<FileInfo>("--output", "File path and name");
//var languageOption = new Option<string[]>("--language", () => new string[] { }, "A list of programming languages to include");
//languageOption.IsRequired = true;



//var bundleCommand = new Command("bundle", "Bundle code file a single file");
//var languageExtensions = new Dictionary<string, string>
//{
//    { ".cs", "c#" },
//    { ".java", "java" },
//    { ".js", "javascript" },
//    { ".ts", "typescript" },
//    { ".py", "python" },

//    // שפות תבנית
//    { ".html", "html" },
//    { ".htm", "html" },
//    { ".css", "css" },
//    { ".scss", "scss" },

//    // שפות שאילתות
//    { ".sql", "sql" },

//    // שפות תסריט
//    { ".sh", "bash" },
//    { ".ps1", "powershell" },
    
//    // שפות אחרות
//    { ".json", "json" },
//    { ".xml", "xml" },
   

//    // ... הוסף התאמות נוספות לפי הצורך
//};
////Autocomplete

//bundleCommand.AddOption(bundleOption);
//bundleCommand.AddOption(languageOption);

//bundleCommand.SetHandler(async (output, languages) =>
//{
//    try
//    {
//        if (File.Exists(output.FullName))
//        {
//            Console.WriteLine("File name exists already, change it and try again...");
//            return;
//        }
//        using (var stream = File.Create(output.FullName))
//        {
//            await stream.FlushAsync();
//        }
//        Console.WriteLine("File was created");

//        var filesToInclude = Directory.GetFiles(".", "*", SearchOption.AllDirectories) // Search recursively
//            .Where(file =>
//            {
//                var extension = Path.GetExtension(file).ToLower();
//                return languages.Contains("all") ||
//                       languages.Any(lang => languageExtensions.TryGetValue(extension, out var matchedLang) && languages.Contains(matchedLang));
//            });
//        // Handle unknown extensions (optional)
//        var unknownExtensions = filesToInclude.Select(f => Path.GetExtension(f))
//            .Distinct()
//            .Except(languageExtensions.Keys); // Find extensions not in the dictionary

//        if (unknownExtensions.Any())
//        {
//            Console.WriteLine("Warning: Found files with unknown extensions:");
//            foreach (var ext in unknownExtensions)
//            {
//                Console.WriteLine($"- {ext}");
//            }
//            Console.WriteLine("These files will not be included in the bundle.");
//        }

//        // Write content to the output file (specific logic needed based on language types)
//        /////////////////////////////////////////////////////////////////
//        if (File.Exists(output.FullName))
//        {
//            try
//            {
//                using (File.Open(output.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
//                {
//                    // הקובץ פנוי
//                }
//            }
//            catch (IOException)
//            {
//                Console.WriteLine("The file is currently in use. Please close any programs that might be using it.");
//                return;
//            }
//        }
//        //////////////////////////////////////////////////////////////////////
//        using (var writer = new StreamWriter(output.FullName, false)) // יוצר את הקובץ אם אינו קיים
//        {
//            foreach (var file in filesToInclude)
//            {
//                Console.WriteLine($"Adding file: {file}");
//                var content = await File.ReadAllTextAsync(file);
//                await writer.WriteLineAsync(content);
//            }
//        }

//        Console.WriteLine("Bundling completed successfully!");

//    }
//    catch (DirectoryNotFoundException ex)
//    {
//        Console.WriteLine("Error: Invalid file path.");
//    }
//    catch (IOException ex)
//    {
//        Console.WriteLine($"Error: The file is already in use or cannot be accessed. Details: {ex.Message}");
//    }
//    catch (Exception ex) // Catch other potential exceptions
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//    }

//}, bundleOption, languageOption);

//var rootCommand = new RootCommand("Root command for File Bundler CLI");

//rootCommand.AddCommand(bundleCommand);

//rootCommand.InvokeAsync(args);

