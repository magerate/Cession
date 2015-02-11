//namespace Cession.Mediators
//{
//	using System;
//
//	using Cession.Commands;
//	using Cession.Modeling;
//
//	public class DiagramCommandMediator
//	{
//		private CommandManager commandManager;
//
//		public DiagramCommandMediator (CommandManager commandManager)
//		{
//			if (null == commandManager)
//				throw new ArgumentNullException ();
//			this.commandManager = commandManager;
//		}
//
//		public void RegisterProjectEvents(Project project){
//			foreach (var layer in project.Layers) {
//				RegisterLayerEvents (layer);
//			}
//		}
//
//		public void UnregisterProjectEvents(Project project){
//			foreach (var layer in project.Layers) {
//				UnregisterLayerEvents (layer);
//			}
//		}
//
//		public void RegisterLayerEvents(Layer layer){
//			layer.AddHandler (Room.DoorRemovedEvent, 
//				new EventHandler<DoorRemovedEventArgs> (OnDoorRemoved));
//		}
//
//		public void UnregisterLayerEvents(Layer layer){
//			layer.RemoveHandler(Room.DoorRemovedEvent,
//				new EventHandler<DoorRemovedEventArgs> (OnDoorRemoved));
//		}
//
//		private void OnDoorRemoved(object sender,DoorRemovedEventArgs e){
//			if (e.IsSideEffect) {
//				var room = e.OriginalSource as Room;
//				var command = new OneArgumentCommand<Door> (e.Door, room.RemoveDoor, room.AddDoor);
//				commandManager.Queue (command);
//			}
//		}
//	}
//}
//
