import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FileSettings } from './file-settings';
//import { DataModel } from './data-model';
import { RowModel } from './row-model';

@Injectable()
export class DataService {

  private url = "http://localhost:21881";
  private urlArray = this.url + "/api/ArrayData";
  private urlFile = this.url + "/api/File";
  //private url2 = "/api/apartments/getpaged2";

  constructor(private http: HttpClient) {
  }

  //Получить начальные данные
  getFileData() {
    return this.http.get(this.urlArray + "/Index");
  }

  //Получить данные из базы
  getArrayData(filePath: string, firstRowIsColumns: boolean, separator: string) {
    return this.http.get(this.urlArray + "/GetArray?filePath=" + filePath + "&firstRowIsColumns=" + firstRowIsColumns + "&separator=" + separator);
  }

  //Сохранить столбцы
  saveColumns(model: RowModel) {
    return this.http.put(this.urlArray + "/SaveColumns/", model);
  }

  //Сохранить строку
  saveRow(model: RowModel) {
    return this.http.put(this.urlArray + "/UpdateRow/", model);
  }

  //Добавить строку
  addRow(model: RowModel) {
    return this.http.put(this.urlArray + "/AddRow/", model);
  }

  //Удалить строку
  removeRow(model: RowModel) {
    return this.http.post(this.urlArray + "/RemoveRow/", model);
  }

  //Удалить строку
  createFile(model: FileSettings) {
    return this.http.post(this.urlFile + "/CreateFile", model);
  }

  //Получить список в папке
  getFiles() {
    return this.http.get(this.urlFile + "/GetFiles");
  }

}
