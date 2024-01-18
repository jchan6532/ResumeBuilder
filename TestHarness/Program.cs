using System;
using System.IO;
using Microsoft.Office.Interop.Word;

class Program
{
    static void Main(string[] args)
    {
        string currentDirectory = Environment.CurrentDirectory;
        Application wordApp = new Application();
        Document doc = wordApp.Documents.Add();

        // Create a table with 2 rows and 1 column
        Table table = doc.Tables.Add(doc.Range(0, 0), 2, 1);

        // Set the height of the first row to Auto
        table.Rows[1].HeightRule = WdRowHeightRule.wdRowHeightAuto;

        // Set the table to span the entire page width
        table.PreferredWidth = 100;
        table.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPercent;

        // Access the second row's first cell
        Cell cell = table.Rows[2].Cells[1];

        // Add text to the cell
        cell.Range.Text = "Work Experience";

        // Apply Heading Level 3 style and set font
        object styleHeading3 = "Heading 3";
        cell.Range.Paragraphs[1].set_Style(ref styleHeading3);
        cell.Range.Paragraphs[1].Range.Font.Name = "Verdana";
        cell.Range.Paragraphs[1].Range.Font.Size = 10;

        // Access the paragraph for the additional text
        Paragraph textPara = cell.Range.Paragraphs.Add();
        textPara.Range.Text = "some text";

        // Save and close the document
        string filePath = Path.Combine(currentDirectory, "Document.docx");
        doc.SaveAs2(filePath);
        doc.Close();
        wordApp.Quit();
    }

    static void InsertBulletPoint()
    {
        string currentDirectory = Environment.CurrentDirectory;


        Application wordApp = new Application();
        object missing = System.Reflection.Missing.Value;

        // Open your template document
        string templatePath = Path.Combine(currentDirectory, "Template.docx");
        Document doc = wordApp.Documents.Open(templatePath, ref missing, ref missing, ref missing);

        // Access the second row of the first table
        Table table = doc.Tables[1];
        Row row = table.Rows[2];

        // Iterate through each cell in row 2
        foreach (Cell cell in row.Cells)
        {
            foreach (Paragraph para in cell.Range.Paragraphs)
            {
                // Check if the paragraph is a Level 3 heading
                if (para.get_Style() is Style style && style.NameLocal == "Heading 3")
                {

                    // Insert bullet point within the cell, just after the heading
                    para.Range.InsertParagraphAfter(); // Insert a new paragraph after the heading
                    Range bulletRange = para.Range.Next(WdUnits.wdParagraph, 1);
                    bulletRange.Text = "bullet point here\r\n"; // Add bullet point text
                    bulletRange.ListFormat.ApplyBulletDefault();

                    // Set font to Calibri and font size to 10
                    bulletRange.Font.Name = "Calibri";
                    bulletRange.Font.Size = 10;
                }
            }
        }

        // Save and close the document
        string newFilename = Path.Combine(currentDirectory, "NewDocument.docx");
        doc.SaveAs2(newFilename);
        doc.Close(ref missing, ref missing, ref missing);
        wordApp.Quit(ref missing, ref missing, ref missing);
    }
}
