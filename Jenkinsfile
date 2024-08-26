pipeline {
    agent any  // Run on any available agent

    stages {
        stage('Checkout Code') {
            steps {
                git(url: 'https://github.com/your/repo.git', branch: 'master')
            }
        }

        stage('Build and Run Services') {
            steps {
                script {
                    sh 'docker-compose up -d --build'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'docker-compose exec -T your_service_name dotnet test'
                }
            }
        }

        stage('Cleanup') {
            steps {
                script {
                    sh 'docker-compose down'
                }
            }
        }
    }

    post {
        always {
            junit '**/test_results.trx'
        }
    }
}
