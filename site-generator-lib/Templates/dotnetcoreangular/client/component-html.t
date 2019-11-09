<h1 id="tableLabel">{{Name}}</h1>

<p *ngIf="!forecasts"><em>Loading...</em></p>

<table class='table table-striped' aria-labelledby="tableLabel" *ngIf="data">
  <thead>
    <tr>
	{{#Fields}}
		<th>{{Name}}</th>
	{{/Fields}}      
    </tr>
  </thead>
  <tbody>
    <tr *ngFor="let entry of data">
	{{#Fields}}
		<th>{{"{{"}}data.{{Name}}{{"}}"}}</th>
	{{/Fields}}
    </tr>
  </tbody>
</table>
