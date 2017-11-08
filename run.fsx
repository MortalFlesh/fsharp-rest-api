#if INTERACTIVE

open System

#I @"/Users/chromecp/.nvm/versions/node/v8.4.0/bin/"

#r "Microsoft.Azure.Webjobs.Host.dll"
//open Microsoft.Azure.WebJobs.Host

#r "System.Net.Http.Formatting.dll"
#r "System.Web.Http.dll"
#r "System.Net.Http.dll"
#r "Newtonsoft.Json.dll"

#else

#r "System.Net.Http"
#r "Newtonsoft.Json"

#endif

// ^^^ code above is for local development only ^^^

open System.Text
open System.Net
open System.Net.Http

type Mark = string
type Value = string
type Template = string

let mark (mark: Mark) (value: Value) (template: Template) =
  template.Replace(sprintf "{{ %s }}" mark, value)

let Run(req: HttpRequestMessage, template: Template, name: Value) =
  let html =
    template
      |> mark "title" "Profile"
      |> mark "name" name
  
  let response = req.CreateResponse(HttpStatusCode.OK)
  response.Content <- new StringContent(html, Encoding.UTF8, "text/html")
  response
