node("marcelbenders.de") {
    def mvnHome
    def commitId
    properties([gitLabConnection('GitLab')])

    stage('Preparation') { 
        checkout scm
        commitId = sh(returnStdout: true, script: 'git rev-parse HEAD')
    }

}
