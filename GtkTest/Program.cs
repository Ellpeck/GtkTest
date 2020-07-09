using System;
using GLib;
using Gtk;
using Application = Gtk.Application;

namespace GtkTest {
    internal static class Program {

        [STAThread]
        private static void Main() {
            Application.Init();
            var window = new Window("Test!");
            window.DeleteEvent += (o, args) => Application.Quit();
            window.DefaultWidth = 500;

            var toolbarBox = new VBox(false, 5);
            window.Add(toolbarBox);

            // toolbar
            var toolbar = new Toolbar {Style = ToolbarStyle.Icons};
            toolbarBox.PackStart(toolbar, false, false, 0);
            toolbar.Add(new ToolButton(Stock.New));
            toolbar.Add(new ToolButton(Stock.Open));
            toolbar.Add(new ToolButton(Stock.Save));
            toolbar.Add(new ToolButton(Stock.SaveAs));
            toolbar.Add(new SeparatorToolItem());
            var quit = new ToolButton(Stock.Quit);
            toolbar.Add(quit);
            quit.Clicked += (o, args) => Application.Quit();

            // pane that can be dragged to change its size
            var pane = new HPaned();
            toolbarBox.PackStart(pane, false, false, 0);

            // vertically aligned box
            var box = new VBox(false, 5);
            pane.Pack2(box, false, false);
            for (var i = 0; i < 10; i++) {
                var button = new Button("Test " + i);
                // put buttons into the box from the top
                box.PackStart(button, false, false, 0);
                button.Clicked += (o, args) => {
                    var dialog = new MessageDialog(window, DialogFlags.Modal, MessageType.Info, ButtonsType.OkCancel, "How are you doing today?");
                    dialog.Response += (o2, args2) => Console.WriteLine("We responded " + args2.ResponseId);
                    // running the dialog pauses execution
                    dialog.Run();
                    // dispose the dialog after we're done
                    dialog.Dispose();
                };
            }

            // file tree
            // takes in the stuff that is displayed in the tree, in order
            var tree = new TreeStore(typeof(string), typeof(string));
            var test1 = tree.AppendValues("Test 1!", "Here");
            tree.AppendValues(test1, "Test 1 Sub!", "There");
            tree.AppendValues("Test 2!", "Somewhere");
            var test3 = tree.AppendValues("Test 3!", "Yea");
            tree.AppendValues(test3, "Test 3 Sub 1!", "Stuff");
            var test3Sub2 = tree.AppendValues(test3, "Test 3 Sub 2!", "Okay");
            tree.AppendValues(test3Sub2, "Test 3 Sub 2 Sub!", "Cool");

            // file tree view which displays the tree
            var view = new TreeView(tree);
            view.AppendColumn("Test Thing", new CellRendererText(), "text", 0);
            view.AppendColumn("Info", new CellRendererText(), "text", 1);
            pane.Pack1(view, false, false);

            window.ShowAll();
            Application.Run();
        }

    }
}