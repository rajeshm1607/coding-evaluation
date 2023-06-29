using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            //Added code for the Hiring fuctionality
            return FindAndHire(root, person, title);
        }
        
        private Position? FindAndHire(Position position, Name person, string title)
        {
            if (position.GetTitle() == title && !position.IsFilled())
            {
                Employee employee = new Employee(position.GetHashCode(), person);
                position.SetEmployee(employee);
                return position;
            }

            foreach (Position directReport in position.GetDirectReports())
            {
                Position? hiredPosition = FindAndHire(directReport, person, title);
                if (hiredPosition != null)
                    return hiredPosition;
            }

            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
