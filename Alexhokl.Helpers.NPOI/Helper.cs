using System;
using NPOI.SS.UserModel;


namespace Alexhokl.Helpers.NPOI
{
    public static class Helper
    {
        /// <summary>
        /// Adds the cell.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="cellValue">The cell value.</param>
        public static void AddCell(this IRow row, object cellValue)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            ICell cell = row.GetCell(row.PhysicalNumberOfCells);
            if (cell == null)
                cell = row.CreateCell(row.PhysicalNumberOfCells);

            if (cellValue != null)
            {
                if (cellValue.GetType() == typeof(string))
                {
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(Convert.ToString(cellValue));
                }
                else if (cellValue.GetType() == typeof(int))
                {
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(cellValue));
                }
                else if (cellValue.GetType() == typeof(bool))
                {
                    cell.SetCellType(CellType.Boolean);
                    cell.SetCellValue(Convert.ToBoolean(cellValue));
                }
            }
        }
    }
}
