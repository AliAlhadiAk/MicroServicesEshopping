pipeline {
  agent any
  stages {
    stage('Checkout Code') {
      steps {
        git(url: 'https://github.com/AliAlhadiAk/MicroServicesEshopping', branch: 'master')
      }
    }

    stage('Shell Script') {
      steps {
        sh 'ls -la'
      }
    }

  }
}