
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
	public class SetNavigateCursor : CursorOnDragSetter
	{
		protected override CursorStateMachine.State CursorState => CursorStateMachine.State.Navigate;

		protected override int Priority => 100;
	}
}
