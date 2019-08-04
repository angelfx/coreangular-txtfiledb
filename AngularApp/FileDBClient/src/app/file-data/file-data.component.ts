import { Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from './file-data.service';
import { DataModel } from './data-model'; //Заменить на свою модель
import { FileSettings } from './file-settings';
import { ArrayDataComponent } from './array-data.component';

@Component({
  selector: 'app-file-data',
  templateUrl: './file-data.component.html',
  providers: [DataService]
})

export class FileDataComponent implements OnInit {

  fileData: FileSettings = new FileSettings();
  arrayShow: boolean = false;
  otherSeparator: boolean = false;
  files: string[];
  filePath: string;
  newFile: boolean = false;

  colsNumber: number = 1;

  @ViewChild('arrayData', { static: false }) arrayDataComponent: ArrayDataComponent;

  constructor(private dataService: DataService) {
  }

  ngOnInit() {
    this.loadFileData();
  }

  //Выбираем сепаратор
  radioSelected(text: string) {
    this.arrayDataComponent.dataModel = new DataModel();
    if (text === 'other') {
      this.otherSeparator = true;
      this.fileData.separator = '';
    }
    else {
      this.otherSeparator = false;
    }
  }

  //Открыть модальное окно с выбором доступных файлов
  openFilesSelector() {
    this.filePath = this.fileData.filePath;

    this.dataService.getFiles()
      .subscribe((data: string[]) => this.files = data);
    var modal = document.getElementById('exampleModal');
    modal.style.display = 'block';
  }

  //Закрыть выбор файлов
  closeFiles() {
    this.filePath = "";
    var modal = document.getElementById('exampleModal');
    modal.style.display = 'none';
  }

  //Выбрать файл
  selectFile() {
    this.fileData.filePath = this.filePath;
    this.fileData.fileName = this.filePath.replace(/^.*[\\\/]/, '')
    var modal = document.getElementById('exampleModal');
    modal.style.display = 'none';
  }

  //Загрузить данные из файла
  loadFileData() {
    this.dataService.getFileData()
      .subscribe((data: FileSettings) => this.fileData = data);
  }

  changeNewFile(evt) {
    var target = evt.target;
    if (target.checked) {
      this.arrayDataComponent.dataModel = new DataModel();
      this.fileData.fileName = "";
      this.fileData.filePath = "";
    } else {
      this.loadFileData();
    }
  }

  createFile() {
    //Отправить запрос на создание файла. Передать название файла и количество столбцов.
    this.fileData.colsNumber = this.colsNumber;

    this.dataService.createFile(this.fileData)
      .subscribe(data => {
        this.newFile = false;
        this.fileData.filePath = "Data\\" + this.fileData.fileName;
        alert("Файл создан");
      });
  }

  //Отобразить данные в дочернем компоненте
  childShow() {
    if (this.fileData.separator == undefined || this.fileData.separator === '') {
      alert('Не задан сепаратор');
      return;
    }
    if (this.fileData.filePath == undefined || this.fileData.filePath === '') {
      alert('Выберете файл');
      return;
    }
    this.arrayDataComponent.loadArray();

  }
}
