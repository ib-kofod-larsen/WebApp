using System.Collections.Generic;

namespace ikl_web.Data
{
    public class DrawingService
    {
        private readonly ICollection<Drawing> _drawings;

        public DrawingService(ICollection<Drawing> drawings)
        {
            _drawings = drawings;
        }
    }
}