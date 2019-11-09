import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-{{Name}}',
  templateUrl: './{{Name}}.component.html'
})
export class {{Name}}Component {
  public data: {{Name}}[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<{{Name}}[]>(baseUrl + 'api/{{Name}}').subscribe(result => {
      this.data = result;
    }, error => console.error(error));
  }
}

interface {{Name}} {
	{{#Fields}}
		{{Name}}: string;
	{{/Fields}}
}
