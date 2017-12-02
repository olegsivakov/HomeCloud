export class Storage {

	private id: string = "";
	private name: string = "";
	private size: number = 0;
	private quota: number = 0;

	get ID(): string {
		return this.id;
	}
	set ID(value: string) {
		this.id = value;
	}

	get Name(): string {
		return this.name;
	}
	set Name(value: string) {
		this.name = value;
	}

	get Size(): number {
		return this.size;
	}
	set Size(value: number) {
		this.size = value;
	}

	get Quota(): number {
		return this.quota;
	}
	set Quota(value: number) {
		this.quota = value;
	}
}
