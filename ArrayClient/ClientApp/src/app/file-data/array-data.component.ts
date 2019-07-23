import { Input, Component, OnInit } from '@angular/core';
import { DataService } from './file-data.service';
import { DataModel } from './data-model';
import { RowModel } from './row-model'; 
import { FileSettings } from './file-settings';
//import { FileSettings } from './file-settings';

@Component({
  selector: 'app-array-data',
  templateUrl: './array-data.component.html',
  providers: [DataService]
})

export class ArrayDataComponent implements OnInit {

  @Input() filePath: string;
  @Input() firstRowIsColumns: boolean;
  @Input() separator: string;

  colsEdit: boolean = false;
  rowEdit: boolean = false;
  rowIndex: number = -1;
  editableRow: string[];
  dataModel: DataModel = new DataModel();
  rowModel: RowModel = new RowModel();
  fileParameters: FileSettings = new FileSettings();

  addingRow: boolean;

  constructor(private dataService: DataService) { }


  ngOnInit() {
 
  }

  //Получить данные
  loadArray() {
    this.dataService.getArrayData(this.filePath, this.firstRowIsColumns, this.separator)
      .subscribe((data: DataModel) => this.dataModel = data);
  }

  //Заполнить модель
  populateModel() {
    this.fileParameters = new FileSettings();
    this.fileParameters.filePath = this.filePath;
    this.fileParameters.firstRowIsColumns = this.firstRowIsColumns;
    this.fileParameters.separator = this.separator;
  }

  trackByFn(index: any, item: any) {
    return index;
  }

  //Удаление строки
  removeRow(index: number) {
    this.populateModel();

    this.rowModel.FileParameters = this.fileParameters;
    this.rowModel.indexRow = index;

    this.dataService.removeRow(this.rowModel)
      .subscribe(data => this.loadArray());
  }

  //Добавление новой строки
  addingNewRow() {
    this.addingRow = true;
    this.editableRow = [];
    for (let i = 0; i < this.dataModel.columns.length; i++) {
      this.editableRow.push('');
    }
  }

  //Отмена добавления строки
  cancelAddNewRow() {
    this.addingRow = false;
    this.editableRow = [];
  }

  //Добавить новую строку
  addNewRow(row: string[]) {
    this.populateModel();

    this.rowModel.FileParameters = this.fileParameters;
    this.rowModel.row = row;

    this.dataService.addRow(this.rowModel)
      .subscribe(data => this.loadArray());

    this.cancelAddNewRow();
  }

  //Редактирование строки
  editCurrentRow(index: number) {
    this.rowEdit = true;
    this.rowIndex = index;
    this.editableRow = Array.from(this.dataModel.data[index]);
  }

  //Сохранения строки
  saveRow(row: string[]) {
    this.populateModel();

    this.rowModel.FileParameters = this.fileParameters;
    this.rowModel.indexRow = this.rowIndex;
    this.rowModel.row = row;

    this.dataService.saveRow(this.rowModel)
      .subscribe(data => this.loadArray());

    this.cancelEditRow();
  }

  //Отмена редактирования строки
  cancelEditRow() {
    this.rowEdit = false;
    this.rowIndex = -1;

    this.editableRow = [];
  }

  //Редактирование столбцов
  editCols() {
    this.colsEdit = true;
  }
  
  //Сохраняем столбцы
  saveCols() {
    this.populateModel();

    this.rowModel.FileParameters = this.fileParameters;
    this.rowModel.row = this.dataModel.columns;

    this.dataService.saveColumns(this.rowModel)
      .subscribe(data => this.loadArray());

    this.cancelEditCols();
  }

  //Отмена редактирования столбцов
  cancelEditCols() {
    this.colsEdit = false;
  }
}
