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

    stage('Build & Test') {
      parallel {
        stage('Build & Test') {
          steps {
            sh '''sh \'dotnet build\'
'''
          }
        }

        stage('Test') {
          steps {
            sh '''sh \'dotnet test --logger "trx;LogFileName=test_results.trx"\'
'''
          }
        }

      }
    }

  }
}