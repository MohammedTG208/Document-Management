@Pipe({ name: 'docIcon' })
export class DocIconPipe implements PipeTransform {
  transform(type: string): string {
    switch (type.toLowerCase()) {
      case 'pdf': return 'fa fa-file-pdf';
      case 'docx': return 'fa fa-file-word';
      case 'xlsx': return 'fa fa-file-excel';
      default: return 'fa fa-file';
    }
  }
}
