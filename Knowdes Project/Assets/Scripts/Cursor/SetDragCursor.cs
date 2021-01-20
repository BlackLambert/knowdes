
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetDragCursor : CursorOnDragSetter
	{
		protected override CursorStateMachine.State CursorState => CursorStateMachine.State.Dragging;

		protected override int Priority => 100;
	}
}
