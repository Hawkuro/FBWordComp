// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Configuration
open System.IO

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let FBDataFolder = ConfigurationManager.AppSettings.Item("FBDataFolder") |> DirectoryInfo
    let messageFolder = FBDataFolder.GetDirectories() |> Array.find (fun d -> d.Name = "messages")
    let messageThreads = messageFolder.GetFiles()

    messageThreads
    |> Array.map (fun f -> f.Name)
    |> printfn "%A" 

    System.Console.ReadLine () |> ignore
    0 // return an integer exit code
