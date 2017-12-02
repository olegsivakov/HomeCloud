import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { HttpBase } from './httpBase.service';

@Injectable()
export class FileService extends HttpBase {
	constructor(
		private http: Http
	) {
		super();
	}
}

