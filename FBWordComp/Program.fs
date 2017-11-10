// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Configuration
open System.IO
open FSharp.Data

let (|UserDiv|_|) (node:HtmlNode) =
    if node.HasClass("message")
    then 
        let mh = node.Elements("div") |> List.tryFind (fun e -> e.HasClass("message_header"))

        if mh.IsSome
        then
            let user = mh.Value.Elements("span") |> List.tryFind (fun e -> e.HasClass("user")) 

            if user.IsSome
            then
                Some(user.Value.InnerText())
            else None
        else None
    else None

let (|Message|_|) (node:HtmlNode) =
    if node.Name() = "p"
    then
        Some(node.InnerText())
    else None


let getUserMessages userName (file:FileInfo) =
    let fileContents = HtmlDocument.Load(file.FullName)
    let thread = fileContents.Body().Elements("div") |> List.find (fun n -> n.HasClass("thread"))

    let rec getUserMessages (nodes:HtmlNode list) (results: string list) =
        match nodes with
        | UserDiv(user)::Message(m)::tail when user = userName -> getUserMessages tail (m::results)
        | UserDiv(_)::Message(_)::tail -> getUserMessages tail results
        | [] -> results
        | _ -> failwith ("file failed to parse: "+file.FullName)

    getUserMessages (thread.Elements(["div";"p"])) []
    //thread.Elements(["div";"p"]) |> List.map (fun e -> e.Name())

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let FBDataFolder = ConfigurationManager.AppSettings.Item("FBDataFolder") |> DirectoryInfo
    let messageFolder = FBDataFolder.GetDirectories() |> Array.find (fun d -> d.Name = "messages")
    let messageThreads = messageFolder.GetFiles()
    let userName, words =
        match Array.toList argv with
        | head::tail -> head, Set.ofList tail
        | _ -> failwith "missing arguments" 

//    messageThreads
//    |> Array.map (fun f -> f.Name)
//    |> printfn "%A" 

    let results =
        messageThreads
        |> Seq.ofArray
        |> Seq.collect (getUserMessages userName)
        |> Seq.toList

    results |> printfn "%A"

    System.Console.ReadLine () |> ignore
    0 // return an integer exit code
