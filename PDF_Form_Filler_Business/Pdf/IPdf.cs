using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Form_Filler_Business.Pdf
{
    public interface IPdf
    {
        byte[] fill(object item);
    }
}
