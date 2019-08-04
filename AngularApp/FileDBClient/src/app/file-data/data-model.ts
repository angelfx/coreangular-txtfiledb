import { FileSettings } from './file-settings';

//Описывает данные
export class DataModel {
  constructor(
    public data?: string[][],
    public columns?: string[],
    public FileParameters: FileSettings = new FileSettings()
  ) { }
}
