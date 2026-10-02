using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Laibrary_Payment_App.Reports
{
    public static class ReportExportHelper
    {
        public static void ExportToExcel(DataGridView dgv, string reportTitle, string? summaryInfo = null)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export - Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv",
                FileName = $"{reportTitle.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.xls",
                Title = "Export Report"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                if (sfd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    ExportToCsv(dgv, sfd.FileName, reportTitle, summaryInfo);
                }
                else
                {
                    ExportToHtmlExcel(dgv, sfd.FileName, reportTitle, summaryInfo);
                }

                var openFile = MessageBox.Show(
                    "Report exported successfully!\n\nWould you like to open the exported file now?",
                    "Export Complete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (openFile == DialogResult.Yes)
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ExportToHtmlExcel(DataGridView dgv, string filePath, string reportTitle, string? summaryInfo)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\">");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; color: #1e293b; }");
            sb.AppendLine("h2 { color: #0f172a; margin-bottom: 4px; }");
            sb.AppendLine(".sub { color: #64748b; font-size: 13px; margin-bottom: 16px; }");
            sb.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 10px; font-size: 13px; }");
            sb.AppendLine("th { background-color: #1e3a5f; color: white; padding: 10px 8px; text-align: left; border: 1px solid #0f172a; font-weight: 600; }");
            sb.AppendLine("td { padding: 8px; border: 1px solid #cbd5e1; text-align: left; }");
            sb.AppendLine("tr:nth-child(even) { background-color: #f8fafc; }");
            sb.AppendLine(".num { text-align: right; }");
            sb.AppendLine(".footer { margin-top: 20px; font-size: 11px; color: #94a3b8; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            sb.AppendLine($"<h2>LIBRARY MANAGEMENT SYSTEM - {System.Net.WebUtility.HtmlEncode(reportTitle.ToUpper())}</h2>");
            sb.AppendLine($"<div class=\"sub\">Generated on: {DateTime.Now:dd-MMM-yyyy hh:mm tt}{(summaryInfo != null ? " | " + System.Net.WebUtility.HtmlEncode(summaryInfo) : "")}</div>");

            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr>");
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!col.Visible) continue;
                string alignClass = col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight ? " class=\"num\"" : "";
                sb.AppendLine($"<th{alignClass}>{System.Net.WebUtility.HtmlEncode(col.HeaderText)}</th>");
            }
            sb.AppendLine("</tr></thead>");
            sb.AppendLine("<tbody>");

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine("<tr>");
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible) continue;
                    var val = row.Cells[col.Index].FormattedValue?.ToString() ?? string.Empty;
                    string alignClass = col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight ? " class=\"num\"" : "";
                    sb.AppendLine($"<td{alignClass}>{System.Net.WebUtility.HtmlEncode(val)}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("<div class=\"footer\">Confidential - Generated by Library Management System (Phase 6 Reporting & Analytics)</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static void ExportToCsv(DataGridView dgv, string filePath, string reportTitle, string? summaryInfo)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\"LIBRARY MANAGEMENT SYSTEM - {reportTitle}\"");
            sb.AppendLine($"\"Generated on: {DateTime.Now:dd-MMM-yyyy hh:mm tt}\"");
            if (!string.IsNullOrWhiteSpace(summaryInfo))
            {
                sb.AppendLine($"\"{summaryInfo}\"");
            }
            sb.AppendLine();

            // Header row
            var headers = new StringBuilder();
            bool firstCol = true;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!col.Visible) continue;
                if (!firstCol) headers.Append(",");
                headers.Append($"\"{col.HeaderText.Replace("\"", "\"\"")}\"");
                firstCol = false;
            }
            sb.AppendLine(headers.ToString());

            // Data rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                var rowData = new StringBuilder();
                firstCol = true;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible) continue;
                    if (!firstCol) rowData.Append(",");
                    var val = row.Cells[col.Index].FormattedValue?.ToString() ?? string.Empty;
                    rowData.Append($"\"{val.Replace("\"", "\"\"")}\"");
                    firstCol = false;
                }
                sb.AppendLine(rowData.ToString());
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public static void PrintReport(DataGridView dgv, string reportTitle, string? filterInfo = null)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Print - Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int currentRowIndex = 0;
            int pageNumber = 1;

            var printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = dgv.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 6;
            printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            printDoc.BeginPrint += (s, e) =>
            {
                currentRowIndex = 0;
                pageNumber = 1;
            };

            printDoc.PrintPage += (s, e) =>
            {
                var g = e.Graphics;
                if (g == null) return;

                int y = e.MarginBounds.Top;
                int startX = e.MarginBounds.Left;
                int printableWidth = e.MarginBounds.Width;

                using var titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
                using var subFont = new Font("Segoe UI", 9, FontStyle.Regular);
                using var headerFont = new Font("Segoe UI", 9, FontStyle.Bold);
                using var cellFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);
                using var footerFont = new Font("Segoe UI", 8, FontStyle.Italic);

                // Print header on each page
                g.DrawString($"LIBRARY MANAGEMENT SYSTEM - {reportTitle.ToUpper()}", titleFont, Brushes.MidnightBlue, startX, y);
                y += 24;

                string subText = $"Generated: {DateTime.Now:dd-MMM-yyyy hh:mm tt}" + (!string.IsNullOrWhiteSpace(filterInfo) ? $" | {filterInfo}" : "");
                g.DrawString(subText, subFont, Brushes.DimGray, startX, y);
                y += 20;

                g.DrawLine(new Pen(Color.MidnightBlue, 1.5F), startX, y, startX + printableWidth, y);
                y += 8;

                // Calculate visible column widths proportional to DataGridView widths
                int visibleColCount = 0;
                int totalDgvWidth = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        visibleColCount++;
                        totalDgvWidth += col.Width;
                    }
                }

                if (totalDgvWidth == 0) totalDgvWidth = 1;

                // Draw Table Header
                int rowHeight = 24;
                g.FillRectangle(new SolidBrush(Color.FromArgb(240, 244, 248)), startX, y, printableWidth, rowHeight);
                g.DrawRectangle(Pens.SteelBlue, startX, y, printableWidth, rowHeight);

                int x = startX;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible) continue;
                    int colWidth = (int)((float)col.Width / totalDgvWidth * printableWidth);
                    var rect = new Rectangle(x + 4, y + 4, colWidth - 8, rowHeight - 8);

                    var format = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight
                            ? StringAlignment.Far : StringAlignment.Near,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    g.DrawString(col.HeaderText, headerFont, Brushes.Black, rect, format);
                    g.DrawLine(Pens.LightSteelBlue, x + colWidth, y, x + colWidth, y + rowHeight);
                    x += colWidth;
                }
                y += rowHeight;

                // Draw Rows
                while (currentRowIndex < dgv.Rows.Count)
                {
                    var row = dgv.Rows[currentRowIndex];
                    if (row.IsNewRow)
                    {
                        currentRowIndex++;
                        continue;
                    }

                    if (y + rowHeight > e.MarginBounds.Bottom - 30)
                    {
                        // Needs next page
                        e.HasMorePages = true;
                        // Draw footer
                        g.DrawString($"Page {pageNumber}", footerFont, Brushes.Gray, startX + printableWidth - 60, e.MarginBounds.Bottom - 20);
                        pageNumber++;
                        return;
                    }

                    // Alternating background
                    if (currentRowIndex % 2 == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(248, 250, 252)), startX, y, printableWidth, rowHeight);
                    }
                    g.DrawRectangle(Pens.Gainsboro, startX, y, printableWidth, rowHeight);

                    x = startX;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!col.Visible) continue;
                        int colWidth = (int)((float)col.Width / totalDgvWidth * printableWidth);
                        var val = row.Cells[col.Index].FormattedValue?.ToString() ?? string.Empty;
                        var rect = new Rectangle(x + 4, y + 4, colWidth - 8, rowHeight - 8);

                        var format = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight
                                ? StringAlignment.Far : StringAlignment.Near,
                            Trimming = StringTrimming.EllipsisCharacter
                        };

                        g.DrawString(val, cellFont, Brushes.Black, rect, format);
                        g.DrawLine(Pens.Gainsboro, x + colWidth, y, x + colWidth, y + rowHeight);
                        x += colWidth;
                    }

                    y += rowHeight;
                    currentRowIndex++;
                }

                // Page footer
                g.DrawString($"Page {pageNumber}", footerFont, Brushes.Gray, startX + printableWidth - 60, e.MarginBounds.Bottom - 20);
                e.HasMorePages = false;
            };

            using var preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 950,
                Height = 650,
                StartPosition = FormStartPosition.CenterParent
            };
            preview.ShowDialog();
        }
    }
}
