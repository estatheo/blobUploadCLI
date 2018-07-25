using System;
using System.Linq;
using System.Threading.Tasks;

namespace blob_image_uploader
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var blob = new BlobService();
            var command = Console.ReadLine();
            while (command != "exit")
            {
                if (command != null)
                    if (command == "help")
                    {
                        Console.WriteLine(
                            "commands: \n upload [options] \n options: \n -file:  location of the file to upload ( upload -file=c:\\images\\flower.jpeg ) \n -cs: connection string of the blob ( upload -cs=... ) \n -r: blob reference ( upload -r=imagescontainer )  ");
                    }
                    else if (command.StartsWith("upload"))
                    {
                        var arguments = command.Split(' ').Where(x => x != "upload").ToList();
                        var file = "";
                        try
                        {
                            file = arguments.Single(x => x.StartsWith("-file")).Split('=').Last();
                        }
                        catch
                        {
                            if (file == "")
                            {
                                Console.WriteLine("insert the file path: ");
                                file = Console.ReadLine();
                            }
                        }

                        var connectionString = "";
                        try
                        {
                            connectionString = arguments.Single(x => x.StartsWith("-cs")).Split('=').Last();
                        }
                        catch
                        {
                            if (connectionString == "")
                            {
                                Console.WriteLine("insert the connection string");
                                connectionString = Console.ReadLine();
                            }
                        }

                        var reference = "";
                        try
                        {
                            reference = arguments.Single(x => x.StartsWith("-r")).Split("=").Last();
                        }
                        catch
                        {
                            if (reference == "")
                            {
                                Console.WriteLine("insert the blob reference");
                                reference = Console.ReadLine();
                            }
                        }
                        
                        Console.WriteLine(await blob.Upload(file, connectionString, reference));
                    }

                command = Console.ReadLine();
            }

            if (args.Length > 2)
            {
                var arguments = args.ToList();
                var file = arguments.Single(x => x.StartsWith("-file")).Split('=').Last();
                var connectionString = arguments.Single(x => x.StartsWith("-cs")).Split('=').Last();
                var reference = arguments.Single(x => x.StartsWith("-r")).Split("=").Last();

                Console.WriteLine(await blob.Upload(file, connectionString, reference));
            }

            Console.ReadLine();
        }
    }
}