using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace penizula_POS.Class.GUI
{
    class clsUnFocusButton : Button
    {
	public clsUnFocusButton() : base()
	{
		this.SetStyle(ControlStyles.Selectable, false);
	}
    }
}
