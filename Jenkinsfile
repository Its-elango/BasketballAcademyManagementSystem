pipeline {
    agent any
  
    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out code from GitHub...'
                checkout scm
            }
        }
      
        stage('Restore NuGet Packages') {
            steps {
                echo 'Restoring NuGet packages...'
                bat "${NUGET} restore YourProject.sln"
            }
        }  
      
        stage('Build') {
            steps {
                echo 'Building the application...'
                bat "${MSBUILD} BasketballAcademy.sln /p:Configuration=Release /p:Platform=\"Any CPU\""
            }
        }  
               
        stage('Deploy') {
            steps {
                echo 'Deploying the application...'
                // Include deployment steps here, e.g., copying files to a deployment directory
                // bat "xcopy /s /y /i YourProject\\bin\\Release C:\\Path\\To\\Deployment\\Directory"
            }
        }    
    }
}
