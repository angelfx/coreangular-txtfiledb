import { FileSettings } from './file-settings';

//Описывает строку
export class RowModel {
  constructor(
    public indexRow?: number,
    public row?: string[],
    public FileParameters: FileSettings = new FileSettings()
  ) { }
}
