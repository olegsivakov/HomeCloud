export class File {
	private id: string = "";
	private name: string = "";
	private size: number = 0;
	private creationDate: Date = new Date();
	private mimeType: string = "";

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

	get CreationDate(): Date {
		return this.creationDate;
	}
	set CreationDate(value: Date) {
		this.creationDate = value;
	}

	get Size(): number {
		return this.size;
	}
	set Size(value: number) {
		this.size = value;
	}

	get MimeType(): string {
		return this.mimeType;
	}
	set MimeType(value: string) {
		this.mimeType = value;
	}
}
