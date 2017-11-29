open System
open System.Windows.Forms
open System.Drawing
open Drawing

let main = new Form(Width = 620, Height = 450, Text = "Pie Chart")
let menu = new ToolStrip()
let btnOpen = new ToolStripButton("Open")
//let btnSave = new ToolStripButton("Save", Enabled = false)
menu.Items.Add(btnOpen) |> ignore
//menu.Items.Add(btnSave) |> ignore

let img =
    new PictureBox
        (BackColor = Color.White, Dock = DockStyle.Fill,
        SizeMode = PictureBoxSizeMode.CenterImage)

main.Controls.Add(menu)
main.Controls.Add(img)

let openAndDrawChart(e) =
    let dlg = new OpenFileDialog(Filter="CSV Files|*.csv")
    if (dlg.ShowDialog() = DialogResult.OK) then
    let bmp = drawChart(dlg.FileName)
    img.Image <- bmp
    //btnSave.Enabled <- true

//let saveDrawing(e) =
//    let dlg = new SaveFileDialog(Filter="PNG Files|*.png")
//    if (dlg.ShowDialog() = DialogResult.OK) then
//    img.Image.Save(dlg.FileName)

[<STAThreadAttribute>]
do
    btnOpen.Click.Add(openAndDrawChart)
    //btnSave.Click.Add(saveDrawing)
    Application.Run(main)

//Asia, 44579000
//Africa, 30065000
//North America, 24256000
//South America, 17819000
//Antarctica, 13209000
//Europe, 9938000
//Australia/Oceania, 7687000