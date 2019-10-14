# YouTube API Integration Angular

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 7.2.0.

- If you don't have node.js installed on your computer, download it [here](https://nodejs.org/en/download/)

- Install angular on your computer.
```
npm install -g @angular/cli
```

- Open the project and run

```
npm install
```

- Go to project src and create an folder called 'environment' and a file 'environment.ts'

- Go to [Firebase Console](https://console.firebase.google.com) and project settings

- Scroll down until you see your Firebase SDK snippet

- Copy your firebaseConfig and replace '=' to ':', see example bellow
```
  firebaseConfig: {
    apiKey: '',
      authDomain: '',
      databaseURL: '',
      projectId: '',
      storageBucket: '',
      messagingSenderId: '',
      appId: ''
  }
```

- Go to your 'environment.ts' file and paste this firebaseConfig after production: false

```
export const environment = {
  production: false,
  firebaseConfig: {
    apiKey: '',
      authDomain: '',
      databaseURL: '',
      projectId: '',
      storageBucket: '',
      messagingSenderId: '',
      appId: ''
  }
};

```

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.
