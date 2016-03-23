// Learn more about F# at http://fsharp.net
open System.Data.SQLite


let InsertProduct() = 
    let conn = new SQLiteConnection("data source=D:\SqlLiteFiles\Robert.db3")
    conn.Open()
    use cmd = conn.CreateCommand()
    [1..100]|> Seq.iter (fun m ->
        cmd.CommandText <- sprintf "insert into Product(Code,Quntity) values('%s','%s')" (m.ToString()) (m.ToString())
        cmd.ExecuteNonQuery()|> ignore
        ()
        )


do
    
    InsertProduct()
    