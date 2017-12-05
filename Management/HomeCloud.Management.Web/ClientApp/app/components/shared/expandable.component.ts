export class ExpandableComponent {
	public opened: boolean = false;

	public toggleMenu() {
		this.opened = !this.opened;
	}
}