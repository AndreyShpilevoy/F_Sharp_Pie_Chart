open System
open System.IO

let convertStringRowToDataItem (stringRow:string) =
    let cells = List.ofSeq (stringRow.Split ',')
    match cells with
    | lable::value::_ ->
        let integerValue = Int32.Parse value
        (lable, integerValue)
    | _ -> failwith "Incorrect data format!"


let rec processLinesToDataItemList lines =
    match lines with
    | [] -> []
    | stringRow::tail ->
        let dataItem = convertStringRowToDataItem stringRow
        let rest = processLinesToDataItemList tail
        dataItem::rest

let rec countSum rows =
   match rows with
   | [] -> 0
   | (_, n)::tail ->
       let sumRest = countSum(tail)
       n + sumRest

[<EntryPoint>]
let main argv =
    let lines = List.ofSeq(File.ReadAllLines(@"C:\data.csv"))
    let data = processLinesToDataItemList(lines)
    let sum = float(countSum(data))

    for (lbl, num) in data do
        let perc = int((float(num)) / sum * 100.0)
        printfn "%-18s - %8d (%d%%)" lbl num perc
 
    0 // return an integer exit code


//Asia, 44579000//Africa, 30065000//North America, 24256000//South America, 17819000//Antarctica, 13209000//Europe, 9938000//Australia/Oceania, 7687000