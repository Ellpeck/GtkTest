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
            window.Resize(500, 500);

            // vertically aligned box
            var box = new VBox(false, 5);
            window.Add(box);
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
            window.ShowAll();
            Application.Run();
        }

    }
}