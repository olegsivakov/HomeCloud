import { Component, OnInit } from '@angular/core';
import { Storage } from '../../models/storage';

@Component({
	selector: 'storage-list',
	templateUrl: './storage-list.component.html'
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

		let storage = new Storage();
		storage.Name = "Test";
		storage.Size = 1024;
		storage.Quota = 2048;

		storages[0] = storage;

		return storages;
	}
}
