module Drawing

open System
open System.Drawing
open Logic
open System.IO

let fnt = new Font("Times New Roman", 11.0f)
let centerX, centerY = 300.0, 200.0
let labelDistance = 150.0
let drawLabel (gr:Graphics) lbl startAngle angle =
    let lblAngle = float(startAngle + angle/2)
    let ra = Math.PI * 2.0 * lblAngle / 360.0
    let x = centerX + labelDistance * cos(ra)
    let y = centerY + labelDistance * sin(ra)
    let size = gr.MeasureString(lbl, fnt)
    let rc = new PointF(float32(x) - size.Width / 2.0f, float32(y) - size.Height / 2.0f)
    gr.DrawString(lbl, fnt, Brushes.Black, new RectangleF(rc, size))


let rnd = new Random()
let randomBrush () =
    let r, g, b = rnd.Next(256), rnd.Next(256), rnd.Next(256)
    new SolidBrush(Color.FromArgb(r,g,b))

let drawPieSegment (gr:Graphics) lbl startAngle angle =
    let br = randomBrush()
    gr.FillPie(br, 170, 70, 260, 260, startAngle, angle)
    br.Dispose()

let drawStep drawingFunc (gr:Graphics) sum data =
    let rec drawStepUtil data angleSoFar =
        match data with
        | [] -> ()
        | [lbl, num] ->
           let angle = 360 - angleSoFar
           drawingFunc gr lbl angleSoFar angle
        | (lbl, num)::tail ->
            let angle = int((float(num)) / sum * 360.0)
            drawingFunc gr lbl angleSoFar angle
            drawStepUtil tail (angleSoFar + angle)
    drawStepUtil data 0

let drawChart file =
    let lines = List.ofSeq(File.ReadAllLines(file))
    let data = processLinesToDataItemList(lines)
    let sum = float(countSum(data))
    let bmp = new Bitmap(600, 400)
    let gr = Graphics.FromImage(bmp)
    gr.FillRectangle(Brushes.White, 0, 0, 600, 400)
    drawStep drawPieSegment gr sum data |> ignore
    drawStep drawLabel gr sum data |> ignore
    gr.Dispose()
    bmp