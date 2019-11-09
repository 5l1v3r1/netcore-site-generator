using Fluid;
using HandlebarsDotNet;
using Ionic.Zip;
using site_generator_lib.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace site_generator_lib.Generators
{
    public static class CoreApiGenerator
    {
        public static bool GenerateWebsite(Website website)
        {
            var assembly = typeof(Generators.CoreApiGenerator).GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();

            using (ZipFile zipFile = ZipFile.Read(assembly.GetManifestResourceStream("site_generator_lib.Templates.dotnetcoreangular.WebApplication.zip")))
            {
                FileInfo fileInfo = new FileInfo(assembly.Location);
                string serverPath = Path.Combine(fileInfo.DirectoryName, $"output{DateTime.Now.ToString("_ddMMyyyy_HHmmss")}", "server");
                zipFile.ExtractAll(serverPath, ExtractExistingFileAction.OverwriteSilently);

                var fullModel = new { Entities = website.Entities };
                
                ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.startup.t", $"{Path.Combine(serverPath, "WebApplication")}\\Startup.cs", fullModel);

                //Cient Generation
                ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.client.nav-menu.t", $"{Path.Combine(serverPath, "WebApplication", "ClientApp", "src", "app", "nav-menu")}\\nav-menu.component.html", fullModel);
                ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.client.module.t", $"{Path.Combine(serverPath, "WebApplication", "ClientApp", "src", "app")}\\app.module.ts", fullModel);


                foreach (var entityModel in website.Entities)
                {
                    var model = new { Name = entityModel.Name, Fields = entityModel.Fields.Select(field => new { Type = Shared.TypeConverter.PostgresToCsharp(field.Type), Name = field.Name }).ToArray() };

                    HandlebarsBlockHelper _propertyList = (TextWriter output, HelperOptions options, dynamic context, object[] arguments) => {
                        var enumList = arguments[0] as object[];
                        for (int i = 0; i < enumList.Length; i++)
                        {
                            string text = $"{((dynamic)(enumList[i])).Type} _{((dynamic)(enumList[i])).Name}";
                            if (i < enumList.Length - 1)
                                text += ",";
                            output.WriteLine(text);
                        }
                    };
                    Handlebars.RegisterHelper("prop_list", _propertyList);

                    HandlebarsBlockHelper _addModelList = (TextWriter output, HelperOptions options, dynamic context, object[] arguments) => {
                        var enumList = arguments[0] as object[];
                        for (int i = 0; i < enumList.Length; i++)
                        {
                            string text = $"addModel.{((dynamic)(enumList[i])).Name}";
                            if (i < enumList.Length - 1)
                                text += ",";
                            output.WriteLine(text);
                        }
                    };
                    Handlebars.RegisterHelper("addmodel_list", _addModelList);
                                        
                    // Build Database Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "WebApplication", "Database", entityModel.Name));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.dbentitycontext.t", $"{Path.Combine(serverPath, "WebApplication", "Database")}\\{entityModel.Name}DbContext.cs", model);
                  
                    // Build Model Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "WebApplication", "Model", "Models", $"{entityModel.Name}Model"));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.model.t", $"{Path.Combine(serverPath, "WebApplication", "Model", "Models")}\\{entityModel.Name}.cs", model);
               
                    // Build Web Controller Layer
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.controller.t", $"{Path.Combine(serverPath, "WebApplication", "Controllers")}\\{entityModel.Name}Controller.cs", model);


                    // Client //////////////////
                    Directory.CreateDirectory(Path.Combine(serverPath, "WebApplication", "ClientApp", "src", "app", entityModel.Name));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.client.component-ts.t", $"{Path.Combine(serverPath, "WebApplication", "ClientApp", "src", "app", entityModel.Name)}\\{entityModel.Name}.component.ts", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcoreangular.client.component-html.t", $"{Path.Combine(serverPath, "WebApplication", "ClientApp", "src", "app", entityModel.Name)}\\{entityModel.Name}.component.html", model);

                }

            }

            return true;
        }

        public static bool GenerateWebsiteFull(Website website)
        {
            var assembly = typeof(Generators.CoreApiGenerator).GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();

            using (ZipFile zipFile = ZipFile.Read(assembly.GetManifestResourceStream("site_generator_lib.Templates.dotnetcore.dotnetcore_template.zip")))
            {
                FileInfo fileInfo = new FileInfo(assembly.Location);
                string serverPath = Path.Combine(fileInfo.DirectoryName, $"output{DateTime.Now.ToString("_ddMMyyyy_HHmmss")}", "server");
                zipFile.ExtractAll(serverPath, ExtractExistingFileAction.OverwriteSilently);

                foreach (var entityModel in website.Entities)
                {
                    var model = new { Name = entityModel.Name, Fields = entityModel.Fields.Select(field => new { Type = Shared.TypeConverter.PostgresToCsharp(field.Type), Name = field.Name }).ToArray() };

                    HandlebarsBlockHelper _propertyList = (TextWriter output, HelperOptions options, dynamic context, object[] arguments) => {
                        var enumList = arguments[0] as object[];
                        for(int i=0; i<enumList.Length; i++)
                        {
                            string text = $"{((dynamic)(enumList[i])).Type} _{((dynamic)(enumList[i])).Name}";
                            if (i < enumList.Length - 1)
                                text += ",";
                            output.WriteLine(text);
                        }
                    };
                    Handlebars.RegisterHelper("prop_list", _propertyList);

                    HandlebarsBlockHelper _addModelList = (TextWriter output, HelperOptions options, dynamic context, object[] arguments) => {
                        var enumList = arguments[0] as object[];
                        for (int i = 0; i < enumList.Length; i++)
                        {
                            string text = $"addModel.{((dynamic)(enumList[i])).Name}";
                            if (i < enumList.Length - 1)
                                text += ",";
                            output.WriteLine(text);
                        }
                    };
                    Handlebars.RegisterHelper("addmodel_list", _addModelList);


                    // Build Domain Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "source", "Domain", entityModel.Name));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.domainentity.t", $"{Path.Combine(serverPath, "source", "Domain", $"{entityModel.Name}")}\\{entityModel.Name}Entity.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.domainentityfactory.t", $"{Path.Combine(serverPath, "source", "Domain", $"{entityModel.Name}")}\\{entityModel.Name}EntityFactory.cs", model);


                    // Build Database Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "source", "Database", entityModel.Name));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.dbirepository.t", $"{Path.Combine(serverPath, "source", "Database", $"{entityModel.Name}")}\\I{entityModel.Name}Repository.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.dbrepository.t", $"{Path.Combine(serverPath, "source", "Database", $"{entityModel.Name}")}\\{entityModel.Name}Repository.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.dbentityconfiguration.t", $"{Path.Combine(serverPath, "source", "Database", $"{entityModel.Name}")}\\{entityModel.Name}EntityConfiguration.cs", model);


                    // Build Model Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model"));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.model.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\{entityModel.Name}Model.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.modelvalidator.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\{entityModel.Name}ModelValidator.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.addmodel.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\Add{entityModel.Name}Model.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.addmodelvalidator.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\Add{entityModel.Name}ModelValidator.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.updatemodel.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\Update{entityModel.Name}Model.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.updatemodelvalidator.t", $"{Path.Combine(serverPath, "source", "Model", "Models", $"{entityModel.Name}Model")}\\Update{entityModel.Name}ModelValidator.cs", model);


                    // Build Application Layer
                    Directory.CreateDirectory(Path.Combine(serverPath, "source", "Application", entityModel.Name));
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.iapplicationservice.t", $"{Path.Combine(serverPath, "source", "Application", entityModel.Name)}\\I{entityModel.Name}ApplicationService.cs", model);
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.applicationservice.t", $"{Path.Combine(serverPath, "source", "Application", entityModel.Name)}\\{entityModel.Name}ApplicationService.cs", model);
                                
                    // Build Web Controller Layer
                    ParseGenerateFileTemplate("site_generator_lib.Templates.dotnetcore.controller.t", $"{Path.Combine(serverPath, "source", "Web", "Controllers")}\\{entityModel.Name}Controller.cs", model);

                }

            }

            return true;
        }

        public static bool ParseGenerateFileTemplate(string templatePath, string outputPath, object objectModel)
        {
            
            var source = ReadResourceAsString(templatePath);
            //"Hello {{ p.Firstname }} {{ p.Lastname }}";

            try
            {
                var template = Handlebars.Compile(source);
                var renderResult = template(objectModel);
                File.WriteAllText(outputPath, renderResult);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return true;
        }

        public static string ReadResourceAsString(string resourcePath)
        {
            var assembly = typeof(Generators.CoreApiGenerator).GetTypeInfo().Assembly;
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourcePath), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
                
        }
    }
}
