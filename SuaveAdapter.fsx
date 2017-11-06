let RunWebPartWithPathAsync app httpRequest = async {
  let! suaveContext = SuaveContext httpRequest
  let netHeaderValue = NetHeaderValue httpRequest.Headers
  match netHeaderValue "X-Suave-URL", netHeaderValue "Host"  with
  | Some suaveUrl, Some host ->
    let url = sprintf "https://%s%s" host suaveUrl |> System.Uri
    let ctx = {suaveContext with
                request = {suaveContext.request with
                            url = url
                            rawQuery = SuaveRawQuery url}}
    return! SuaveRunAsync app ctx
  | _ -> return! SuaveRunAsync app suaveContext
}
