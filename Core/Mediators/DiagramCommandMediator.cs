using System;

using Cession.Commands;
using Cession.Diagrams;
using Cession.Projects;

namespace Cession.Mediators
{
    public class DiagramCommandMediator
    {
        private CommandManager _commandManager;

        public CommandManager CommandManager
        {
            get{ return _commandManager; }
        }

        public DiagramCommandMediator (CommandManager commandManager)
        {
            if (null == commandManager)
                throw new ArgumentNullException ();
            _commandManager = commandManager;
        }

        public DiagramCommandMediator()
        {
            _commandManager = new CommandManager ();
        }

        public void AttachProject (Project project)
        {
            foreach (var layer in project.Layers)
            {
                RegisterLayerEvents (layer);
            }
        }

        public void DetachProject (Project project)
        {
            foreach (var layer in project.Layers)
            {
                UnregisterLayerEvents (layer);
            }
            _commandManager.Clear ();
        }

        public void RegisterLayerEvents (Layer layer)
        {
            layer.LabelLocationChanged += LabelLocationChanged;
//            layer.AddHandler (Room.DoorRemovedEvent, 
//                new EventHandler<DoorRemovedEventArgs> (OnDoorRemoved));
        }

        public void UnregisterLayerEvents (Layer layer)
        {
            layer.LabelLocationChanged -= LabelLocationChanged;
//            layer.RemoveHandler (Room.DoorRemovedEvent,
//                new EventHandler<DoorRemovedEventArgs> (OnDoorRemoved));
        }

//        private void OnDoorRemoved (object sender, DoorRemovedEventArgs e)
//        {
//            if (e.IsSideEffect)
//            {
//                var room = e.OriginalSource as Room;
//                var command = new OneArgumentCommand<Door> (e.Door, room.RemoveDoor, room.AddDoor);
//                commandManager.Queue (command);
//            }
//        }

        private void LabelLocationChanged(object sender,LocationChangedEventArgs e)
        {
            var label = e.OriginalSource as Label;
            var command = Command.Create (label, e.NewLocation, e.OldLocation, (l, p) => l.SetLocation (p, false));
            _commandManager.Queue (command);
        }
    }
}

