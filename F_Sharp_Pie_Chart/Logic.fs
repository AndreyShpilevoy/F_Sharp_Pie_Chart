module Logic

open System

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