#if INTERACTIVE

open System

#I @"/Users/chromecp/.nvm/versions/node/v8.4.0/bin/"

#r "Microsoft.Azure.Webjobs.Host.dll"
open Microsoft.Azure.WebJobs.Host

#r "System.Net.Http.Formatting.dll"
#r "System.Web.Http.dll"
#r "System.Net.Http.dll"
#r "Newtonsoft.Json.dll"

#else

#r "System.Net.Http"
#r "Newtonsoft.Json"

#endif

open System.Text
open System.Net
open System.Net.Http

let Run(req: HttpRequestMessage, template: string, name: string) =
  let html = template.Replace("%name%", name)
  
  let response = req.CreateResponse(HttpStatusCode.OK)
  response.Content <- new StringContent(html, Encoding.UTF8, "text/html")
  response
