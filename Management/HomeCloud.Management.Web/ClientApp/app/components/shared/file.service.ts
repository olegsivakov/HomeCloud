import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

@Injectable()
export class FileService {
	constructor(
		private http: Http
	) { }

	uploadFile() {

	}

	getFiles(catalogId: string, offset: number, limit: number) {
	}


}

