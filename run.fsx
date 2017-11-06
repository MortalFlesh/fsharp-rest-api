#r "System.Net.Http"
#r "Newtonsoft.Json"

open System.Net
open System.Net.Http
open Newtonsoft.Json

type Named = {
    name: string
}

type QueryParam =
    string

let queryParam (req: HttpRequestMessage) (key: QueryParam) =
    req.GetQueryNameValuePairs()
    |> Seq.tryFind (fun q -> q.Key = key)

let createResponse statusCode (req: HttpRequestMessage) message =
    req.CreateResponse(statusCode, message)

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info(sprintf 
            "F# HTTP trigger function processed a request.")

        let queryParam = queryParam req;
        let response = createResponse HttpStatusCode.OK req;
        let badResponse = createResponse HttpStatusCode.BadRequest req;

        let greet name =
            response ("Hello " + name)

        // Set name to query string
        let name = queryParam "name";

        match name with
        | Some x ->
            return greet x.Value;
        | None ->
            let! data = req.Content.ReadAsStringAsync() |> Async.AwaitTask

            if not (String.IsNullOrEmpty(data)) then
                let named = JsonConvert.DeserializeObject<Named>(data)

                return greet named.name;
            else
                return badResponse "Specify a Name value";
    } |> Async.RunSynchronously
