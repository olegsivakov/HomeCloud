import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';

import { DataModel } from './data.model';

@Component({
	selector: 'data',
	templateUrl: './data.component.html',
	styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {
	private _uploader: FileUploader;
	private _hasBaseDropZoneOver: boolean = false;

	private url: string = 'path_to_api';

	public data: Array<DataModel> = new Array<DataModel>();

	constructor() {
	}

	ngOnInit() {
		this._uploader = new FileUploader({ url: this.url });

		let model1 = new DataModel();
		model1.id = "1";
		model1.name = "Test Catalog";
		model1.isDirectory = true;
		model1.imageUrl = "https://image.freepik.com/free-psd/abstract-background-design_1297-87.jpg";
		model1.isSelected = false;
		model1.count = 9;
		this.data.push(model1);

		let model2 = new DataModel();

		model2.id = "2";
		model2.name = "Test File 1.docx";
		model2.isDirectory = false;
		model2.imageUrl = "https://www.shareicon.net/download/2016/06/24/618239_word_2000x2000.png";
		model2.isSelected = false;
		this.data.push(model2);

		let model3 = new DataModel();

		model3.id = "3";
		model3.name = "Test File 2.xlsx";
		model3.isDirectory = false;
		model3.imageUrl = "https://www.shareicon.net/data/2016/06/24/618244_excel_2000x2000.png";
		model3.isSelected = false;
		this.data.push(model3);
	}

	public get uploader(): FileUploader {
		return this._uploader;
	}

	public get hasBaseDropZoneOver(): boolean {
		return this._hasBaseDropZoneOver;
	}

	public get showRenameCommand(): boolean {
		return this.getSelectedModels().length == 1;
	}

	public get showDeleteCommand(): boolean {
		return this.getSelectedModels().length > 0;
	}

	public get showDownloadCommand(): boolean {
		return this.getSelectedModels().length == 1;
	}

	public fileOverBase(e: any): void {
		this._hasBaseDropZoneOver = e;
	}

	public select(model: DataModel): void {
		let item = this.data.find(item => item == model);
		if (item) {
			item.isSelected = !item.isSelected;
		}
	}

	public rename(): void {
		let models: Array<DataModel> = this.getSelectedModels();
	}

	public delete(model: DataModel): void {

	}

	private getSelectedModels(): Array<DataModel> {
		return this.data.filter(item => item.isSelected);
	}
}
