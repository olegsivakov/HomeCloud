export class Storage {
	private static readonly units: Array<string> = ["B", "KB", "MB", "GB", "TB"];

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

	get Progress(): number {
		return this.Size == 0 ? 0 : (this.Size/this.Quota) * 100;
	}

	get QuotaString(): string {
		let quota: number = this.Quota;
		let unitIndex: number = 0;

		while (quota > 1000) {
			quota = quota / 1024;

			unitIndex += 1;
		}

		return Math.round(quota).toFixed(1) + Storage.units[unitIndex];
	}

	get SizeString(): string {
		let size: number = this.Size;
		let unitIndex: number = 0;

		while (size > 1000) {
			size = size / 1024;

			unitIndex += 1;
		}

		return Math.round(size).toFixed(1) + Storage.units[unitIndex];
	}
}
