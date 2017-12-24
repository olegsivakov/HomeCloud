import { Component, OnInit } from '@angular/core';
import { Storage } from '../../models/storage';

@Component({
	selector: 'storage-list',
	templateUrl: './storage-list.component.html',
	styleUrls: [ './storage-list.component.css' ]
})
export class StorageListComponent implements OnInit {
	public storages: Array<Storage>;

	constructor() {
	}

	ngOnInit() {
		this.storages = this.get();
	}

	public get(): Array<Storage> {
		let storages = new Array<Storage>();

		storages[0] = this.getStorage("Aleh Sivakou's storage", 295068491776, 1000202039296);
		storages[1] = this.getStorage("My wife's storage", 730147488686, 1000202039296);
		storages[2] = this.getStorage("My daugther's storage", 73767122944, 147534245888);

		return storages;
	}

	private getStorage(name: string, size: number, quota: number): Storage {
		let storage = new Storage();

		storage.Name = name;
		storage.Size = size;
		storage.Quota = quota;

		return storage;
	}
}
