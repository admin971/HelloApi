pipeline {
    agent any

    environment {
        DOCKER_COMPOSE_FILE = "docker-compose.yml"
    }

    stages {
        stage('Checkout Code') {
            steps {
                // Clone the repository
                git branch: 'main', url: 'https://github.com/admin971/HelloApi.git'
            }
        }

        stage('Build Docker Images') {
            steps {
                script {
                    // Build Docker images
                    sh 'docker-compose build'
                }
            }
        }

        stage('Run Containers') {
            steps {
                script {
                    // Start the containers
                    sh 'docker-compose up -d'
                }
            }
        }

        stage('Test Endpoints') {
            steps {
                script {
                    // Test the root endpoint
                    sh 'curl http://localhost:5158/'

                    // Test database connection
                    sh 'curl http://localhost:5158/dbtest'
                }
            }
        }

        stage('Clean Up') {
            steps {
                script {
                    // Stop and remove containers
                    sh 'docker-compose down'
                }
            }
        }
    }

    post {
        always {
            // Ensure cleanup in case of failure
            sh 'docker-compose down || true'
        }
    }
}
