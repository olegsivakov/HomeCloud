import { Component, OnInit, Input } from '@angular/core';
import { Storage } from '../../models/storage';

@Component({
	selector: 'storage',
	templateUrl: './storage.component.html',
	styleUrls: ['./storage.component.css']
})
export class StorageComponent implements OnInit {
	@Input() storage: Storage;

	constructor() { }

	ngOnInit() {

	}

	public save(): Storage {
		return this.storage;
	}

	public delete() {
	}
}
