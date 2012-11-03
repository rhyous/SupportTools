using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SupportTools.ContextMenuXml;

namespace ContextMenuXmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputerContextMenu menu = new ComputerContextMenu();
            MenuAction action1 = new MenuAction("Action 1", null);
            action1.IsMultiSelect = true;
            action1.Command = "some1.exe";
            action1.Parameters = "param1 param2";
            action1.Filter = new InventoryFilter("Type");
            action1.Filter.AllowedValues.Add("Workstation");
            action1.Filter.AllowedValues.Add("Server");
            menu.MenuItems.Add(action1);

            MenuGroup group1 = new MenuGroup("Group 1", null);
            menu.MenuItems.Add(group1);
            
            MenuAction action2 = new MenuAction("Action 2", null);
            action2.IsMultiSelect = true;
            action2.Command = "some2.exe";
            action2.Parameters = "param1 param2";
            action2.Filter = new InventoryFilter("Type");
            action2.Filter.AllowedValues.Add("Workstation");
            action2.Filter.AllowedValues.Add("Server");
            group1.MenuItems.Add(action2);


            Serializer.SerializeToXML<MenuGroup>(menu, "Test.xml");


            ComputerContextMenu cmi = Serializer.DeserializeFromXML<ComputerContextMenu>("Test.xml");

        }
    }
}
